import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './members/lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AnalyticsComponent } from './analytics/analytics.component';
import { NewsfeedComponent } from './newsFeed-components/newsfeed/newsfeed.component';
import { AuthGuard } from './_guards/auth.guard';
import { ProfilePageComponent } from './members/profile-page/profile-page.component';
import { ProfilePageResolver } from './_resolvers/profile-page.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { ProfileEditComponent } from './members/profile-edit/profile-edit.component';
import { ProfileEditResolver } from './_resolvers/profile-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { FollowerListComponent } from './follower-list/follower-list.component';
import { FollowerResolver } from './_resolvers/followers.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'newsfeed', component: NewsfeedComponent, resolve: {user: FollowerResolver}},
            { path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver}},
            { path: 'analytics', component: AnalyticsComponent},
            { path: 'lists', component: ListsComponent, resolve: {users: MemberListResolver}},
            { path: 'profile/edit', component: ProfileEditComponent, resolve: {user: ProfileEditResolver},
                canDeactivate: [PreventUnsavedChanges]},
            { path: 'followers', component: FollowerListComponent, resolve: {user: FollowerResolver}},
            { path: 'members/:id', component: ProfilePageComponent, resolve: {user: ProfilePageResolver}},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];


