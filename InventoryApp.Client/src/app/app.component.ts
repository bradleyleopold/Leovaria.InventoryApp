import { Component, OnInit, ViewChild } from '@angular/core';
import { ItemService } from './services/item.service';
import { Item } from './models/item';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AddEditItemModalComponent } from './components/modals/add-edit-item-modal/add-edit-item-modal.component';

/**
 * Main component of the app used to display items, as well
 * as giving users functionality to perform operations.
 */
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  /**
   * List of items to display on the page.
   */
  protected itemsList: Item[];

  /**
   * Denotes whether to display the items table or not.
   */
  protected displayTable: boolean;

  /**
   * Dialog for adding/editing items.
   */
  @ViewChild(AddEditItemModalComponent) addEditItemModal!: AddEditItemModalComponent;

  /**
   * Constructor.
   */
  constructor(private itemService: ItemService, private confirmationService: ConfirmationService, private messageService: MessageService) {
    this.itemsList = [];
    this.displayTable = false;
  }

  /**
   * On component initialization.
   */
  ngOnInit() {
    this.itemService.getAll().subscribe(result => {
      this.itemsList = result;
      this.displayTable = true;
    });
  }

  /**
   * Opens a delete confirmation for the selected item.
   * @param item Item that was selected to delete.
   */
  protected openItemDeleteConfirmation(item: Item): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: "p-button-danger p-button-text",
      rejectButtonStyleClass: "p-button-text p-button-text",
      acceptIcon: "none",
      rejectIcon: "none",
      accept: () => {
        this._deleteItem(item);
        this.messageService.add({ severity: 'info', summary: 'Deletion confirmed', detail: 'Record has been deleted' });
      }
    });
  }

  /**
   * Opens the dialog for adding a new item.
   */
  protected openAddItemDialog(): void {
    this.addEditItemModal.openDialogAdd();
  }

  /**
   * Opens the dialog for editing an existing item.
   * @param item Item to edit.
   */
  protected openEditItemDialog(item: Item): void {
    this.addEditItemModal.openDialogEdit(item);
  }

  /**
   * Deletes the item from the database.
   * @param item Item to delete.
   */
  private _deleteItem(item: Item): void {
    this.itemService.delete(item).subscribe();

    // Remove the deleted item from the page list
    // so that it doesn't show in the results.
    const deletedItemIndex = this.itemsList.findIndex(x => x.id === item.id);
    this.itemsList.splice(deletedItemIndex, 1);
  }

  /**
   * Adds a new item to the item list for the page.
   * @param item Item to add.
   */
  protected itemAdded(item: Item) {
    this.itemsList.push(item);
    this.messageService.add({ severity: 'info', summary: 'Add confirmed', detail: 'Record has been added' });
  }

  /**
   * Edits an existing item in the items list for the page.
   * @param item The updated item to replace the existing with.
   */
  protected itemEdited(item: Item) {
    const itemToUpdateIndex = this.itemsList.findIndex(x => x.id === item.id);
    this.itemsList[itemToUpdateIndex] = item;
    this.messageService.add({ severity: 'info', summary: 'Update confirmed', detail: 'Record has been updated' });
  }
}
