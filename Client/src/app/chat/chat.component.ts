import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageDto } from './../Dtos/MessageDto';
import { Component, OnInit } from '@angular/core';
import { ChatService } from '../services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  myForm:FormGroup = new FormGroup({});
  constructor(private chatService: ChatService) {
    this.myForm = new FormGroup({
      content : new FormControl('',Validators.required),
      groupName : new FormControl('',Validators.required)
    });
  }

  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe( 
      (receivedObj: MessageDto) => {
         this.addToInbox(receivedObj);
      }
    );                                                 
  }

  public msgDto: MessageDto = new MessageDto();
  msgInboxArray: MessageDto[] = [];

  // send(): void {
  //   if(this.msgDto) {
  //     if(this.msgDto.user.length == 0 || this.msgDto.user.length == 0){
  //       window.alert("Both fields are required.");
  //       return;
  //     } else {
  //       this.chatService.broadcastMessage(this.msgDto).subscribe(
  //         data => console.log(data)
  //       );
  //     }
  //   }
  // }

  addToInbox(obj: MessageDto) {
    let newObj = new MessageDto();
    newObj.user = obj.user;
    newObj.msgText = obj.msgText;
    this.msgInboxArray.push(newObj);

  }

  public SendToGroup(){
    console.log(this.myForm.value);
    this.chatService.sendMessageToGroup(this.myForm.value).subscribe(
      () => {
        console.log("Message Sent");
      },err => {
        console.log(err);
      }
    );
  }

}
