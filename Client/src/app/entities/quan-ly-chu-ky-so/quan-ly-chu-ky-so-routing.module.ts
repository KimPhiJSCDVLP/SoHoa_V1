import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuanLyChuKySoComponent } from './quan-ly-chu-ky-so/quan-ly-chu-ky-so.component';


const routes: Routes = [
  {
    path: "",
      data: {
          title: "Quản lý Con Dấu Văn Bản"
      },
      children: [
          {
              path: 'chuKySo',
              data: {
                  title: "Con Dấu Văn Bản"
              },
              component: QuanLyChuKySoComponent
          },
          
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuanLyChuKySoRoutingModule { }
