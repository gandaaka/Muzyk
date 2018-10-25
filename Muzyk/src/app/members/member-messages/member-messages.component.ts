import { Component, OnInit, Input } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { tap } from 'rxjs/operators';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @Input()
  recepientId: number;
  public messages: Message[];
  public newMessage: any = {};
  private connection: signalR.HubConnection;

  signalUrl = environment.signalRUrl;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.signalUrl + '/mHub')
      .build();

    this.connection
      .start()
      .then(() => {
        this.alertify.success('Connection Started');
      })
      .catch(error => {
        this.alertify.warning(error);
      });

    this.loadMessages();
  }

  loadMessages() {
    const currentUserId = +this.authService.decodedToken.nameid;
    this.userService
      .getMessageThread(this.authService.decodedToken.nameid, this.recepientId)
      .pipe(
        tap(messages => {
          for (let i = 0; i < messages.length; i++) {
            if (
              messages[i].isRead === false &&
              messages[i].recepientId === currentUserId
            ) {
              this.userService.markAsRead(currentUserId, messages[i].id);
            }
          }
        })
      )
      .subscribe(
        messages => {
          this.messages = messages;
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

  sendMessage() {
    this.newMessage.recepientId = this.recepientId;
    this.userService
      .sendMessage(this.authService.decodedToken.nameid, this.newMessage)
      .subscribe(
        (message: Message) => {
          this.messages.unshift(message);
          /* this.connection.invoke('NewMessage', message.content)
            .then(() => this.loadMessages()); */
          this.newMessage.content = '';
        },
        error => {
          this.alertify.error(error);
        }
      );
  }
}
