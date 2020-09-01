import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';
import { Constants } from 'src/app/Models/Constants';
import { Validators, FormGroup, FormControl } from '@angular/forms';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute,private route: Router, private userService:UserService
    ) { }

  ngOnInit(): void {
  }
  Form = new FormGroup({
    Email: new FormControl('',[Validators.required,Validators.pattern(Constants.EmailReg)]),
    Password: new FormControl('',Validators.required),
  });
  submit(){
    console.log(this.Form.value);
    this.userService.login(this.Form.value).subscribe(res=>{
        this.userService.setToken(res.data);
        this.route.navigateByUrl('/home');
        if(res.data!=null){
        }
        
    },(error)=>{
      console.log(error);
    })
  };

}
