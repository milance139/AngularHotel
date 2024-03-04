import { IRoom } from "./Room"
import { ICurrency } from "./Currency"
import { IUser } from "./User"
export interface IReservation {
  id?: number,
  from: Date;
  to: Date;
  reservationCommitteeId: number;
  reservationCommittee: IUser;
  reservationCommitteeFullName?: string
  originalPrice: number;
  discount: number;
  totalPrice: number;
  currencyId?: number;
  currency: ICurrency;
  isCancelled: boolean;
  reservedRooms: IRoom[];
}
