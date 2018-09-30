import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './members/lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AnalyticsComponent } from './analytics/analytics.component';
import { NewsfeedComponent } from './newsfeed/newsfeed.component';
import { AuthGuard } from './_guards/auth.guard';
import { ProfilePageComponent } from './members/profile-page/profile-page.component';
import { ProfilePageResolver } from './_resolvers/profile-page.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'newsfeed', component: NewsfeedComponent},
            { path: 'messages', component: MessagesComponent},
            { path: 'analytics', component: AnalyticsComponent},
            { path: 'lists', component: ListsComponent, resolve: {users: MemberListResolver}},
            { path: 'members/:id', component: ProfilePageComponent, resolve: {user: ProfilePageResolver}},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];


