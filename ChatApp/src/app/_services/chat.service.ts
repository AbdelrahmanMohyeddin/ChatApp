import { AccountService } from './account.service';
import { MessageToGroupRequest } from './../_models/MessageToGroupRequest';
import { environment } from './../../environments/environment';
import { Group } from './../_models/group';
import { ElementRef, Injectable, OnInit, ViewChild } from '@angular/core';
import * as signalR from '@microsoft/signalr';          // import signalR
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';


const baseUrl = `${environment.apiUrl}`;
@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private  connection: any = new signalR.HubConnectionBuilder().withUrl(`${baseUrl}/chatsocket`)   // mapping to the chathub as in startup.cs
  .configureLogging(signalR.LogLevel.Information)
  .build();
  readonly POST_URL = `${baseUrl}/Messenger`;

  private receivedMessageObject: MessageToGroupRequest = new MessageToGroupRequest();
  private sharedObj = new Subject<MessageToGroupRequest>();

  constructor(private http: HttpClient,private User : AccountService) { 
    this.connection.onclose(async () => {
      await this.start();
    });
    
    //this.connection.on("ReceiveOnGroup", (user, message) => { this.mapReceivedMessage(user, message); });
    this.connection.on("ReceiveOnGroup",function(user,mesg){
      const ul = document.getElementById("messages");
      const li = document.createElement('li');
      li.textContent = mesg;
      if(user != User.accountValue.email){
        li.className = "foreign-message";
      }else{
        li.className = "my-message";
      }
      ul.appendChild(li);
      const el = document.getElementById("chat-body");
      
      el.scrollTo(0,el.offsetHeight);
    });
    //this.connection.on("Connected",function(user,mesg){console.log(user+" "+mesg);});
    //this.connection.on("DisConnected",function(user,mesg){console.log(user+" "+mesg);});
    this.start();         
  }

  public newMessage(mesg:any){
    console.log("new message");
    const li =  document.createElement('p');
    //const ul = document.getElementsByClassName('all-messages');
    li.textContent = mesg;
    document.append(li);
  }
  

  // Strart the connection
  public async start() {
    try {
      await this.connection.start();
      console.log("connected");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    } 
  }

  private mapReceivedMessage(  user: string, message: string): void {
  this.receivedMessageObject.UserName = user;
  this.receivedMessageObject.MessageText = message;
  this.sharedObj.next(this.receivedMessageObject);
  }

  /* ****************************** Public Mehods **************************************** */

  // Calls the controller method
  public broadcastMessage(msgDto: MessageToGroupRequest,email:any) {
    msgDto.UserName = email;
    this.http.post(this.POST_URL, msgDto).subscribe(data => console.log(data));
    //this.connection.invoke("SendMessage1", msgDto.UserName, msgDto.MessageText).catch(err => console.error(err));    // This can invoke the server method named as "SendMethod1" directly.
  }


  public retrieveMappedObject(): Observable<MessageToGroupRequest> {
    return this.sharedObj.asObservable();
  }
}
