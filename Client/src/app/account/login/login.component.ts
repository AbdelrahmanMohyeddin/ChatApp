import { Router } from '@angular/router';
import { AccountService } from './../account.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm:FormGroup = new FormGroup({});
  constructor(private service : AccountService,private router : Router) { }

  ngOnInit(): void {
    this.CreateForm();
    // if(localStorage.getItem("token")){
    //   this.router.navigateByUrl("/");
    // }
  }

  CreateForm(){
    this.loginForm = new FormGroup({
      email:new FormControl('',Validators.required),
      password:new FormControl('',Validators.required)
    });
  }

  onSubmit(){
    this.service.Login(this.loginForm.value).subscribe(
      ()=>{
        console.log("Success Login");
      },err=>{
        console.log(err);
      }
    );
  }

}
