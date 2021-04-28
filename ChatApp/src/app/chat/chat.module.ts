import { ChatRoutingModule } from './chat-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicComponent } from './public.component';
import { LayoutComponent } from './layout.component';
import { GroupComponent } from './group.component';
import { PrivateComponent } from './private/private.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    PublicComponent,
    LayoutComponent,
    GroupComponent,
    PrivateComponent
  ],
  imports: [
    CommonModule,
    ChatRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ChatModule { }

