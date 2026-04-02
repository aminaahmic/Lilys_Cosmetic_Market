
//=== ROUTING FILE FOR CLIENT PART ===

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client.component';

const routes: Routes = [
  { path: '', component: ClientComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)], //znači da su ovo child rute za ovaj modul
  exports: [RouterModule]//omogućava da routing bude dostupan modulu koji ga importuje, znači ovaj fajl povezuje URL sa komponentom
})
export class ClientRoutingModule { }