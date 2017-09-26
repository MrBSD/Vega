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
  columns = [
    { title: 'Id' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { }
  ];
  makes: KeyValuePair[];
  vehicles: Vehicle[];
  query: any = {};


  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {

    this.vehicleService.getMakes()
    .subscribe(makes => this.makes=makes);

    this.populateVehicles();
  }

  private populateVehicles(){
    this.vehicleService.getVehicles(this.query)
    .subscribe(vehicles => this.vehicles=vehicles);
    
  }
  onFilterChange(){
  this.populateVehicles();
  }

  resetFilter(){
    this.query={};
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

}
