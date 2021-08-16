import { Group } from './../_models/group';
import { GroupService } from './../_services/group.service';
import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService, ChatService } from '@app/_services';

@Component({templateUrl: 'layout.component.html' })
export class LayoutComponent implements OnInit  {
    public groups : any[];
    groupForm: FormGroup;
    constructor(private service :ChatService,private fb:FormBuilder,private account:AccountService, private gService : GroupService) {
        this.groupForm = this.fb.group({
        'MessageText':new FormControl('',[Validators.required]),
        'GroupName':new FormControl('')
        });
    }
    f(){return this.groupForm.value}

    onSubmit(){
        this.service.broadcastMessage(this.groupForm.value,this.account.accountValue.email);
        console.log(this.groupForm.value);
    }

    ngOnInit(){
        this.gService.getAllGroups()
        .subscribe(g => this.groups = g['result']);
    }

}
