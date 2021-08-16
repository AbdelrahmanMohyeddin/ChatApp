import { first } from 'rxjs/operators';
import { Account } from '@app/_models';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


const baseUrl = `${environment.apiUrl}`;
@Injectable({
  providedIn: 'root'
})

export class GroupService {
  readonly apiUrl = `${baseUrl}/Group`;
  readonly apiURL = `${baseUrl}/Chat`;

  constructor(private http:HttpClient) {
  }

  

  getAllGroups(){
    return this.http.get<any[]>(this.apiUrl);
  }
}
