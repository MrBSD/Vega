import { KeyValuePair } from '../../Models/KeyValuePair';
import { Vehicle } from './../../Models/Vehicle';
import { VehicleService } from '../../Services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  
  columns = [
    { title: 'Id' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { }
  ];
  makes: KeyValuePair[];
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };


  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {

    this.vehicleService.getMakes()
    .subscribe(makes => this.makes=makes);

    this.populateVehicles();
  }

  private populateVehicles(){
    this.vehicleService.getVehicles(this.query)
    .subscribe(result => this.queryResult=result);
    
  }
  onFilterChange(){
    this.query.page=1;
  this.populateVehicles();
  }

  resetFilter(){
    this.query={
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.onFilterChange();
  }

  sortBy(columnName){
    if (this.query.sortBy===columnName){
      this.query.isSortAscending = !this.query.isSortAscending;
    }
    else {
      this.query.sortBy=columnName;
      this.query.isSortAscending = true;
    }

    this.populateVehicles();

  }

  onPageChange(page){
    this.query.page=page;
    this.populateVehicles();
  }

}
