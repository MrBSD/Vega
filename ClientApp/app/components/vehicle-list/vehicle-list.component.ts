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
  allVehicles: Vehicle[];
  vehicles: Vehicle[];
  makes: KeyValuePair[];
  filter: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {

    this.vehicleService.getMakes()
    .subscribe(makes => this.makes=makes);

    this.vehicleService.getVehicles()
      .subscribe(vehicles => this.vehicles=this.allVehicles=vehicles);
  }

  onFilterChange(){
    var filteredVehicles = this.allVehicles
    
    if (this.filter.makeId)
      filteredVehicles=filteredVehicles.filter(v => v.make.id==this.filter.makeId);

    this.vehicles=filteredVehicles;
  }

  resetFilter(){
    this.filter={};
    this.onFilterChange();
  }

}
