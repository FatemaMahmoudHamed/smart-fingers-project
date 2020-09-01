import { Component, OnInit } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular'; 

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
     buttonText:{
       today:    'اليوم',
       month:    'شهر',
       week:     'اسبوع',
       day:      'يوم',
       list:     'قائمة'
     },
     headerToolbar:{      
         left:   'today prev,next',
         center: '',
         right:  'title'      
     },

     titleFormat:{ year: 'numeric', month: 'long' }  ,               
     dayHeaderContent: function(res)
      {
      if(res.text==="Sun")
        return "الأحد";
      else if(res.text==="Sat")  
        return "السبت";
        else if(res.text==="Mon")  
        return "الأثنين";
        else if(res.text==="Tue")  
        return "الثلاثاء";
        else if(res.text==="Wed")  
        return "الأربعاء";
        else if(res.text==="Fri")  
        return "الجمعة";
        else if(res.text==="Thu")  
        return "الخميس";      
    },
   };

  constructor() { }
  ngOnInit(): void {
  }

}
