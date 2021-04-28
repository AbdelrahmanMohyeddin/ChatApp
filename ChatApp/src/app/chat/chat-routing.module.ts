import { PrivateComponent } from './private/private.component';
import { GroupComponent } from './group.component';
import { PublicComponent } from './public.component';
import { ChatModule } from './chat.module';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';


const routes:Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
        { path: 'public', component: PublicComponent },
        { path: 'group' , component: GroupComponent},
        { path: 'private' , component: PrivateComponent}
    ]
}
]

@NgModule({
  imports:[RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class ChatRoutingModule { }
