import { Component, OnInit, Injectable } from '@angular/core';
import { CustomerVM } from 'src/app/Models/CustomerVM';
import{ResponseVM } from 'src/app/Models/ResponseVM';
import { HttpClient } from '@angular/common/http';
import { CustomerService } from 'src/app/Services/customer.service';
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import {
  NgbDateStruct, NgbCalendar, NgbDatepickerI18n
} from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import {ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR} from '@angular/forms';
import { MAT_RADIO_DEFAULT_OPTIONS } from '@angular/material/radio';
import { ToastrService } from 'ngx-toastr';

const WEEKDAYS = ['ن', 'ث', 'ر', 'خ', 'ج', 'س', 'ح'];
const MONTHS = ['يناير','فبراير','مارس','ابريل','مايو','يونيو','يوليو','أغسطس','سبتمبر','أكتوبر','نوفمبر','ديسمبر']

 @Injectable()
export class ArabicI18n extends NgbDatepickerI18n {
  getWeekdayShortName(weekday: number) {
    return WEEKDAYS[weekday-1];
  }
  getMonthShortName(month: number) {
    return MONTHS[month-1];
  }
  getMonthFullName(month: number) {
    return MONTHS[month-1];
  }
  getDayAriaLabel(date: NgbDateStruct): string {
    return `${date.day}-${date.month}-${date.year}`;
  }
}

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
  providers: [    
    {provide: NgbDatepickerI18n, useClass: ArabicI18n},
    {
      provide: MAT_RADIO_DEFAULT_OPTIONS,
      useValue: { color: 'accent' },
  }
  ]
})

export class CustomersComponent implements OnInit {
  constructor( private modalService: NgbModal,
    private datePipe: DatePipe,private formBuilder: FormBuilder,private calendar: NgbCalendar
    ,private  service:CustomerService,private http: HttpClient,private toastr: ToastrService){}
  ManageUser:string="إضافة عميل  جديد";
  model:any;  
  Customers:any;
closeResult: string;  
arrows:any;
birthdate:Date;
selectToday() {
  this.Form.controls["BirthDate"].setValue(this.calendar.getToday().year-50);
}
ngOnInit(): void {
  this.GetAllCustomers();
  this.model=new  CustomerVM();
  this.createForm();
}

  OpenAddUser(UserContent,model) {           
    if(model!=null)
    {
       this.ManageUser="تعديل بيانات العميل";       
      this.model=model;      
      this.birthdate=new  Date(model.birthdate)         
      this.Form = this.formBuilder.group({
        ID:[model.userID],
        FullName: [model.fullName, [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
        BirthDate: [{year:this.birthdate.getFullYear(),month:this.birthdate.getMonth()+1,day:this.birthdate.getDate()},Validators.required ],
        Gender: [model.gender.toString(), [Validators.required]]          
      });  
    }
    else
    {
      this.ManageUser="إضافة عميل جديد";
        this.model=new CustomerVM();
        this.createForm();      
    }
    this.modalService.open(UserContent).result.then(
      result => {
        this.closeResult = `Closed with: ${result}`;
      },
      reason => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }
Form: FormGroup;
  createForm() {     
    console.log(this.model);       
    this.Form = this.formBuilder.group({
      ID:[this.model.ID],
      FullName: [this.model.FullName, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      BirthDate: [this.model.BirthDate,Validators.required ],
      Gender: [this.model.Gender, [Validators.required]]          
    });    
  }  
  AddOrEditUser()
  {
    if(this.model.ID==0)
    {      
      this.model.FullName=this.Form.controls['FullName'].value;
      let date=this.Form.controls['BirthDate'].value;
      var Gender:string=this.Form.controls['Gender'].value;
      this.model.BirthDate=this.datePipe.transform(new Date(date.year,date.month-1,date.day),'yyyy-MM-dd');
      this.model.Gender=Gender;
      this.service.CreateCustomer(this.model).subscribe(result=>{
        if(result!=null)
        {
          this.toastr.success('تم إضافة العميل بنحاج');
          this.GetAllCustomers();
        }
      },error=>{
        console.log(error);
      });
     
    }
    else
    {      
      this.model.FullName=this.Form.controls['FullName'].value;
      let date=this.Form.controls['BirthDate'].value;
      var Gender:string=this.Form.controls['Gender'].value;
      this.model.BirthDate=this.datePipe.transform(new Date(date.year,date.month-1,date.day),'yyyy-MM-dd');
      this.model.Gender=Gender;
      console.log(this.model);     
      this.service.EditCustomer(this.model).subscribe(result=>{
        if(result != null)
        {
          this.toastr.success('تم تعديل العميل بنحاج');
          this.GetAllCustomers();
        }
      },error=>{
        console.log(error);
      });
    }        
  }
  result:ResponseVM;
  GetAllCustomers()
  {
    this.service.getAllCustomers().subscribe(res=>{
        this.Customers=res;
      console.log(this.Customers);
    },error=>{
      console.log(error);      
    });
    
  }

  OpenDeleteUser(Deletecontent,customer)
  {    
    this.model=customer;
    console.log(this.model);
    this.modalService.open(Deletecontent).result.then(
      result => {
        this.closeResult = `Closed with: ${result}`;
      },
      reason => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }
  DeleteUser()
  {
    this.service.DeleteCustomer(this.model.id).subscribe(result=>{
      if(result !=null)
      {
        this.toastr.success('تم مسح العميل بنحاج');
        this.GetAllCustomers();
      }
    },error=>{
      console.log(error);
    })
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return "by pressing ESC";
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return "by clicking on a backdrop";
    } else {
      return `with: ${reason}`;
    }
  }

  }