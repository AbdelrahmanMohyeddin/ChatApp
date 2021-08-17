import { loginDto } from './../Dtos/loginDto';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { IUser } from '../Dtos/userDto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl + "accounts/";
  private CurrentUserSource = new BehaviorSubject<IUser | null>(null);
  CurrentUser$ = this.CurrentUserSource.asObservable();
  constructor(private http:HttpClient,private router : Router) { }


  loadCurrentUser(token:string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<IUser>(this.baseUrl,{headers}).pipe(
      map((User:IUser)=>{
        if(User){
          localStorage.setItem("token",User.token);
          this.CurrentUserSource.next(User);
        }
      })
    );
  }

  Login(values:loginDto){
    console.log("from service "+ values.email);
   return this.http.post<IUser>(this.baseUrl + "login" , values).pipe(
      map((user : IUser) => {
        if(user){
          localStorage.setItem("token",user.token);
          this.CurrentUserSource.next(user);
        }
      })
    );
  }

  Register(values:any){
    return this.http.post<IUser>(this.baseUrl + "register",values).pipe(
      map((user:IUser) => {
        if(user){
          localStorage.setItem("token",user.token);
        }
      })
    );
  }

  Logout(){
    localStorage.removeItem("token");
    this.CurrentUserSource.next(null);
    this.router.navigateByUrl("/");
  }

  CheckEmailExist(email:string){
    return this.http.get(this.baseUrl + "emailexist?email=" + email);
  }



}
