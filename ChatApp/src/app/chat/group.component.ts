import { AccountService } from './../_services/account.service';
import { ChatService } from './../_services/chat.service';
import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({templateUrl: 'group.component.html'})
export class GroupComponent implements OnInit {

  groupForm: FormGroup;
  constructor(private service :ChatService,private fb:FormBuilder,private account:AccountService) {
    this.groupForm = this.fb.group({
      'MessageText':new FormControl('',[Validators.required])
    });
  }
  f(){return this.groupForm.value}

  onSubmit(){
    
    this.service.broadcastMessage(this.groupForm.value,this.account.accountValue.email);
    console.log(this.groupForm.value);
  }

  ngOnInit(): void {
    
  }

}
