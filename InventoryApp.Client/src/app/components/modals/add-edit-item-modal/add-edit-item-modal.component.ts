import { Component, EventEmitter, Output } from '@angular/core';
import { Item } from '../../../models/item';
import { ItemService } from '../../../services/item.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogMode } from '../../../common/enums/dialog-mode-enum';
import { catchError } from 'rxjs';
import { MessageService } from 'primeng/api';

/**
 * Modal for adding or editing an item.
 */
@Component({
  selector: 'app-add-edit-item-modal',
  templateUrl: './add-edit-item-modal.component.html',
  styleUrl: './add-edit-item-modal.component.css'
})
export class AddEditItemModalComponent {

  /**
   * Event emitted when an item is added.
   */
  @Output() itemAddedEvent: EventEmitter<Item> = new EventEmitter<Item>();

  /**
   * Event emitted when an item is modified.
   */
  @Output() itemEditedEvent: EventEmitter<Item> = new EventEmitter<Item>();

  /**
   * Denotes if the modal is visible or not.
   */
  protected isVisible: boolean;

  /**
   * The heading text of the modal.
   */
  protected header: string;

  /**
   * The item being added or modified.
   */
  protected item: Item;

  /**
   * The form group of the modal.
   */
  protected addEditItemFormGroup: FormGroup;

  /**
   * Denotes if the form has been submitted.
   */
  protected isSubmitted: boolean;

  /**
   * Denotes which mode this dialog is currently in.
   */
  protected dialogMode: DialogMode;

  /**
   * Enum values for DialogMode.
   */
  protected DialogMode: typeof DialogMode = DialogMode;

  /**
   * Component constructor.
   */
  constructor(private formBuilder: FormBuilder, private itemService: ItemService, private messageService: MessageService) {
    this.isVisible = false;
    this.isSubmitted = false;
    this.header = '';
    this.dialogMode = DialogMode.add;
    this.item = {} as Item;
    this.addEditItemFormGroup = this.formBuilder.group({
      id: ['00000000-0000-0000-0000-000000000000'],
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', Validators.required],
      quantity: [0, [Validators.required, Validators.min(0)]]
    });
  }

  /**
   * Opens the dialog in "add" mode.
   */
  public openDialogAdd(): void {
    this.header = 'Add Item';
    this.isVisible = true;
    this.dialogMode = DialogMode.add;

    this.addEditItemFormGroup = this.formBuilder.group({
      id: ['00000000-0000-0000-0000-000000000000'],
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', Validators.required],
      quantity: [0, [Validators.required, Validators.min(0)]]
    });

    this._setForm();
  }

  /**
   * Opens the dialog in "edit" mode.
   * @param item The item to edit.
   */
  public openDialogEdit(item: Item): void {
    this.header = 'Edit Item'
    this.dialogMode = DialogMode.edit;
    this.item = item;
    this.isVisible = true;

    this.addEditItemFormGroup = this.formBuilder.group({
      id: [item.id],
      name: [item.name, [Validators.required, Validators.minLength(3)]],
      description: [item.description, Validators.required],
      quantity: [item.quantity, [Validators.required, Validators.min(0)]]
    });

    this._setForm();
  }

  /**
   * Sets up common form items.
   */
  private _setForm(): void {
    this.addEditItemFormGroup.valueChanges.subscribe(values => {
      this.item = values;
    });
  }

  /**
   * Closes the dialog.
   */
  public close(): void {
    this.item = {} as Item;
    this.addEditItemFormGroup.reset();
    this.isVisible = false;
    this.isSubmitted = false;
  }

  /**
   * Submits the form.
   */
  protected submit(): void {
    this.isSubmitted = true;

    // Invalid form, don't continue.
    if (!this.addEditItemFormGroup.valid) {
      return;
    }

    switch (this.dialogMode) {
      case DialogMode.add:
        this._submitAdd();
        break;
      case DialogMode.edit:
        this._submitEdit();
        break;
    }
  }

  /**
   * Submits a POST request of the new item.
   */
  private _submitAdd() {
    this.itemService.post(this.item).pipe(
      catchError(err => {
        this.messageService.add({ severity: 'error', summary: 'Add failed', detail: `${err.error}` });
        throw null;
      }),
    ).subscribe((result) => {
      this.itemAddedEvent.emit(result);
      this.close();
    });
  }

  /**
   * Submits a PUT request of the modified item.
   */
  private _submitEdit() {
    this.itemService.put(this.item).pipe(
      catchError(err => {
        this.messageService.add({ severity: 'error', summary: 'Update failed', detail: `${err.error}` });
        throw null;
      }),
    ).subscribe((result) => {
      this.itemEditedEvent.emit(result);
      this.close();
    });
  }
}
