import { Injectable } from '@angular/core';
import * as signalR  from '@microsoft/signalr'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MessageDto } from '../Dtos/MessageDto';
import { Subject,Observable } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class ChatService {
  connectionId:string = "";
  private  connection: any = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:44393/chatApp")
  .configureLogging(signalR.LogLevel.Information)
  .build();

  readonly POST_URL = "https://localhost:44393/api/chat/"

  private receivedMessageObject: MessageDto = new MessageDto();
  private sharedObj = new Subject<MessageDto>();

  constructor(private http: HttpClient) { 
    this.hubConnection();      
  }

  hubConnection(){
    this.connection.onclose(async () => {
      await this.start();
    });

    this.connection.on("ReceiveOne", (user:any, message:any) => {
       this.mapReceivedMessage(user, message);
    });

    
    this.start(); 
  }


  // Strart the connection
  public async start() {
  try {
    await this.connection.start();
    console.log("connected");

    this.connection.invoke('getConnectionId')
    .then((result:string) => {
      console.log(result);
      this.addNewConnection(result);
    });


  } catch (err) {
    console.log(err);
    setTimeout(() => this.start(), 5000);
    }
    
  }

  private mapReceivedMessage(user: string, message: string): void {
    this.receivedMessageObject.user = user;
    this.receivedMessageObject.msgText = message;
    this.sharedObj.next(this.receivedMessageObject);
  }

  /* ****************************** Public Mehods **************************************** */

  // Calls the controller method
  public broadcastMessage(msgDto: any) {
    return this.http.post(this.POST_URL+"send", msgDto);
  // this.connection.invoke("SendMessage1", msgDto.user, msgDto.msgText).catch(err => console.error(err));    // This can invoke the server method named as "SendMethod1" directly.
  }

  public addNewConnection(connectionId:string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    this.http.get("https://localhost:44393/api/accounts/addingConnection?connectionId="+connectionId,{headers}).subscribe();
  }
  public retrieveMappedObject(): Observable<MessageDto> {
    return this.sharedObj.asObservable();
  }



  loadCurrentUser(token:string = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFiZGVscmFobWFuQGdtYWlsLmNvbSIsImdpdmVuX25hbWUiOiJBYmRlbHJhaG1hbiIsIm5iZiI6MTYyOTE0Mjc0NCwiZXhwIjoxNjI5NzQ3NTQ0LCJpYXQiOjE2MjkxNDI3NDQsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTE1NDcifQ._4WpnzN2ZwAnQiw0zwaAttphL5YvPDgDJRJbuIMRlA8"){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<any>("https://localhost:44393/api/accounts",{headers}).pipe(
      map((User:any)=>{
        if(User){
          console.log(User);
        }
      })
    );
  }



}
