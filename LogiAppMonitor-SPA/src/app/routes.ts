import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { LogMessageListResolver } from './_resolvers/logMessage-list.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'lists', component: ListsComponent, resolve: {logMessages: LogMessageListResolver}}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
