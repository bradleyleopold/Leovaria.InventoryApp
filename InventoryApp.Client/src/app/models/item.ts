/**
 * Model for an item object.
 */
export interface Item {

  /**
   * Primary key id (GUID).
   */
  id: string;

  /**
   * The name of the item.
   */
  name: string;

  /**
   * The description of the item.
   */
  description: string;

  /**
   * The amount of the item currently available.
   */
  quantity: number;
}
