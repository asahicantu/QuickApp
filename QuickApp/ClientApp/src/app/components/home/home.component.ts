// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component, Renderer } from '@angular/core';
import { Utilities } from '../../services/utilities';
import { fadeInOut } from '../../services/animations';
import { AlertService, MessageSeverity } from '../../services/alert.service';
import { AccountService } from '../../services/account.service';
import { ConfigurationService } from '../../services/configuration.service';
import { HomeService } from './home.service';
import { LocalDataSource } from 'ngx-smart-table/lib/data-source/local/local.data-source';
import { NgbDate, NgbCalendar, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Svc } from 'src/app/models/svc.model';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  animations: [fadeInOut],

})
export class HomeComponent {
  hoveredDate: NgbDate;
  fromDate: NgbDate;
  toDate: NgbDate;

  public source: LocalDataSource;
  public settings = {
    delete: {
      deleteButtonContent: 'Delete',
      confirmDelete: true,
    },
    edit: {
      confirmSave: true,
    },
    add: {
      confirmCreate: true,
    },
    columns: {
      //id: { title: 'id' },
      //domainid: { title: 'domainid' },
      //accountid: { title: 'accountid' },
      readableDate: { title: 'Date', sort: true, sortDirection: 'desc' },
      country:
      {
        title: 'Country',
        width: '20%',
        editor: {
          type: 'list',
          config: {
            list: [
              { value: 'NO', title: 'NO' },
              { value: 'DK', title: 'DK' }
            ],
          }
        }
      },

      serviceDeliveryManager: { title: 'SDL', width:'40%'},
      accountManager: { title: 'AM' },
      //{
      //  title: 'Date',
      //  filter: {
      //    type: 'daterange',
      //    config: {
      //      daterange: {
      //        format: 'dd-mm-yyyy'
      //      }
      //    }
      //  }
      //},
      quoteFTL: { title: 'Quote FTL' },
      po: { title: 'PO' },
      client: { title: 'Client', width: '100px' },
      field: { title: 'Field' },
      well: { title: 'Well' },
      au: { title: 'AU' },
      ac: { title: 'AC' },
      portfolio: { title: 'Portfolio' },
      subPortfolio: { title: 'SubPortfolio' },
      masterCode: { title: 'Master Code' },
      currency: { title: 'Currency' },
      fxRate: { title: 'FX Rate' },
      comment: { title: 'Comment' },
      technicalLead: { title: 'Thecnicla Lead' },
      changePointTask: { title: 'ChangePoint Task' },
      rofo: { title: 'ROFO' },
      imf: { title: 'IMF' },
      mmf: { title: 'MMF' },
      sentToInvoice: { title: 'Sent To Invoice' },
      revenue: { title: 'Revenue' },
      invocieNumber: { title: 'Invoice Number' },
      cost: { title: 'Cost' },
      costrRceived: { title: 'Cost Received' },
      costType: { title: 'Cost Type' },
      glAccount: { title: 'GL Account' },
      costDescription: { title: 'Cost Description' }
    }
  };


  constructor(public accountService: AccountService, public alertService: AlertService, public configurations: ConfigurationService, public homeService: HomeService, public calendar: NgbCalendar) {
    this.source = new LocalDataSource();
    this.source.onAdded().subscribe(this.onAddedObserver);
    this.fromDate = calendar.getPrev(calendar.getToday(), 'm', 12);
    this.toDate = calendar.getNext(calendar.getToday(), 'm', 12);
    this.getSvc(this.fromDate, this.toDate);
  }

  onAddedObserver = {
    next: x => console.log('Observer got a next value: ' + x),
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification')
  };

  onDateSelection(date: NgbDate) {
    if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
    } else if (this.fromDate && !this.toDate && date.after(this.fromDate)) {
      this.toDate = date;
    } else {
      this.toDate = null;
      this.fromDate = date;
    }
  }

  isHovered(date: NgbDate) {
    return this.fromDate && !this.toDate && this.hoveredDate && date.after(this.fromDate) && date.before(this.hoveredDate);
  }

  isInside(date: NgbDate) {
    return date.after(this.fromDate) && date.before(this.toDate);
  }

  isRange(date: NgbDate) {
    return date.equals(this.fromDate) || date.equals(this.toDate) || this.isInside(date) || this.isHovered(date);
  }


  canCreateSvc(event) {
    console.log("Can Create");
    console.log(typeof event);
    console.log(event);
  }

  canEditSvc(event) {
    console.log("Can Edit");
    console.log(typeof event);
    console.log(event);
  }

  canDeleteSvc(event) {
    console.log("Can Delete");
    console.log(typeof event);
    console.log(event);
  }


  createSvc(event) {
    console.log("Create");
    this.formatModel(event.newData);
    this.homeService.getCreateSvc<boolean>(event.newData).subscribe((success: boolean) => {
      if (success) {
        this.alertService.showMessage('Success', 'Record Created', MessageSeverity.success);
        event.confirm.resolve();
      }
      else {
        this.alertService.showMessage('Error', 'Failed to create Record', MessageSeverity.error);
      }
    });
  }

  editSvc(event) {
    //this.homeService.getIsLockedSvc<string>(event.newData.id).subscribe((username: string) => {
    //  if (username != null) {
    //    this.alertService.showMessage('Error', 'Cannot update record since is locked by ' + username, MessageSeverity.error);
    //    event.confirm.reject();
    //  }
    //  else {
    this.formatModel(event.newData);
    this.homeService.getUpdateSvc<boolean>(event.newData).subscribe((success: boolean) => {
      if (success) {
        this.alertService.showMessage('Success', 'Record Updated successfully', MessageSeverity.success);
        event.confirm.resolve();
      }
      else {
        this.alertService.showMessage('Error', 'Failed updateRecord', MessageSeverity.error);
      }
    });
  }

  formatModel(modelData) {
    modelData.newDate = modelData.readableDate;
    //modelData.date = Utilities.stringToDate(modelData.readableDate, 'dd/mm/yyyy', '/');
    //console.log(modelData.date);
  }


  deleteSvc(event) {
    if (window.confirm('Are you sure you want to delete?')) {
      var id = event.data.id;
      this.homeService.getDeleteSvc<boolean>(id).subscribe((success: boolean) => {
        if (success) {
          event.confirm.resolve();
          this.alertService.showMessage('Success', 'Record Deleted', MessageSeverity.success);
        }
        else {
          this.alertService.showMessage('Error', 'Failed to delete Record', MessageSeverity.error);
        }
      });
    } else {
      event.confirm.reject();
    }
  }

  getSvc(fromDate?: NgbDate, toDate?: NgbDate) {
    if (fromDate == null || toDate == null) {
      this.homeService.getSvc<Svc[]>().subscribe((data: Svc[]) => this.source.load(data));
    }
    else {
      var from = new Date(fromDate.year, fromDate.month, fromDate.day);
      var to = new Date(toDate.year, toDate.month, toDate.day);
      this.homeService.getSvc<Svc[]>(from, to).subscribe((data: Svc[]) => this.source.load(data));
    }
  }

  resetFilters() {
    this.source.reset();
  }

  refreshData() {
    this.resetFilters();
    this.getSvc(this.fromDate, this.toDate);
  }





}
