import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthorizeGuard} from 'src/api-authorization/authorize.guard';
import {TodoComponent} from './todo/todo.component';

export const routes: Routes = [
  { path: '', component: TodoComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
