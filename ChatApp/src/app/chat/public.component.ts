import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({templateUrl: 'public.component.html'})
export class PublicComponent {
    publicForm : FormGroup;

    constructor(private fb : FormBuilder) {
        this.publicForm = this.fb.group({
            'message':new FormControl('',[Validators.required]) 
        });
        
    }

    onSubmit(){
        console.log(this.publicForm.value);
    }

}
