import { RoomType } from "../enums/RoomType"

export interface IRoom {
  id?: number;
  name?: string;
  code?: string;
  capacity: number;
  description?: string;
  isDeleted: boolean;
  roomType: RoomType;
}
