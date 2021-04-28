import { Group } from './group';

export class MessageDto{
    id:string;
    text:string;
    type:MsgType;
    user:Account
    group:Group
}

export enum MsgType{
    Text,
    Image,
    Pdf
}