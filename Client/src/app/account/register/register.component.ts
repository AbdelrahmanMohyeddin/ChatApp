import { AccountService } from './../account.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm:FormGroup = new FormGroup({});
  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      email:new FormControl('',Validators.required),
      userName:new FormControl('',Validators.required),
      password:new FormControl('',Validators.required),
    });
  }

  onSubmit(){
    this.accountService.Register(this.registerForm.value).subscribe(
      () => {
        console.log("Success Register");
      },err =>{
        console.log(err);
      }
    );
  }
}
