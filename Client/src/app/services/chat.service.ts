import { MessageToGroup } from './../Dtos/MessageToGroup';
import { Injectable } from '@angular/core';
import * as signalR  from '@microsoft/signalr'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MessageDto } from '../Dtos/MessageDto';
import { Subject,Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ChatService {
  readonly baseUrl = environment.apiUrl + "chat";
  private receivedMessageObject: MessageDto = new MessageDto();
  private sharedObj = new Subject<MessageDto>();


  private  connection: any = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:44393/chatApp")
  .configureLogging(signalR.LogLevel.Information)
  .build();

  

  constructor(private http: HttpClient) { 
    this.hubConnection();      
  }

  hubConnection(){
    this.connection.onclose(async () => {
      await this.start();
    });

    this.connection.on("ReceiveOne", (user:any, message:any) => {
      this.receivedMessageObject.user = user;
      this.receivedMessageObject.msgText = message;
      this.sharedObj.next(this.receivedMessageObject);
    });

    this.connection.on("MessageToGroup", (user:any, message:any) =>{
      this.receivedMessageObject.user = user;
      this.receivedMessageObject.msgText = message;
      this.sharedObj.next(this.receivedMessageObject);
      console.log(this.receivedMessageObject);
    })

    this.start(); 
  }

  public async start() {
    try {
      this.http.get(this.baseUrl+"/SignedUser",{headers:this.cloneHeaders()}).subscribe(
        () =>{
          console.log("Success Signed");  
        },err=>{
          console.log(err);
        }
      )

      await this.connection.start();
      console.log("connected");
      this.connection.invoke('getConnectionId')
        .then((result:string) => {
          localStorage.setItem("connectionId",result);
          this.addNewConnection(result);
        }
      );
      
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    }
  }

  //======================================= Requests =====================================

  public broadcastMessage(msgDto: any) {
    return this.http.post(this.baseUrl+"send", msgDto);
  }

  public addNewConnection(connectionId:string){
    this.http.get(this.baseUrl+"/addingConnection?connectionId="+connectionId,{headers : this.cloneHeaders()}).subscribe();
  }

  public sendMessageToGroup(message:MessageToGroup){
    console.log(message);
    return this.http.post(this.baseUrl+"/sendToGroup",{message},{headers:this.cloneHeaders()});
  }


//=========================================== Retrieve Data ===============================

  public retrieveMappedObject(): Observable<MessageDto> {
    return this.sharedObj.asObservable();
  }

//============================================== Helper ========================================
  
  public cloneHeaders(){
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem("token")}`
    })
  }
}
