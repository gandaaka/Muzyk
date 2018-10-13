import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { AgmCoreModule } from '@agm/core';

import {
  BsDropdownModule,
  TabsModule,
  ProgressbarModule,
  BsDatepickerModule,
  PaginationModule,
  ButtonsModule,
  CarouselModule
} from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { TimeAgoPipe } from 'time-ago-pipe';

import { appRoutes } from './routes';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { ListsComponent } from './members/lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AnalyticsComponent } from './analytics/analytics.component';
import { NewsfeedComponent } from './newsfeed/newsfeed.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { ProfilePageComponent } from './members/profile-page/profile-page.component';
import { ProfilePageResolver } from './_resolvers/profile-page.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { ProfileEditComponent } from './members/profile-edit/profile-edit.component';
import { ProfileEditResolver } from './_resolvers/profile-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { AutoGeneratedComponent } from './auto-generated/auto-generated.component';
import { FollowerListComponent } from './follower-list/follower-list.component';
import { FollowerResolver } from './_resolvers/followers.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { MemberMapComponent } from './members/member-map/member-map.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      ListsComponent,
      MessagesComponent,
      AnalyticsComponent,
      NewsfeedComponent,
      MemberCardComponent,
      ProfilePageComponent,
      ProfileEditComponent,
      PhotoEditorComponent,
      AutoGeneratedComponent,
      MemberMessagesComponent,
      TimeAgoPipe,
      FollowerListComponent,
      MemberMapComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      TabsModule.forRoot(),
      BsDropdownModule.forRoot(),
      CarouselModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      AgmCoreModule.forRoot({
        apiKey: ''
      }),
      ProgressbarModule.forRoot(),
      BsDatepickerModule.forRoot(),
      PaginationModule.forRoot(),
      ButtonsModule.forRoot(),
      NgxGalleryModule,
      FileUploadModule,
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGetter,
          whitelistedDomains: ['localhost:5000'],
          blacklistedRoutes: ['localhost:5000/api/auth/register']
        }
      })
    ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AlertifyService,
    AuthGuard,
    PreventUnsavedChanges,
    UserService,
    ProfilePageResolver,
    MemberListResolver,
    ProfileEditResolver,
    FollowerResolver,
    MessagesResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
