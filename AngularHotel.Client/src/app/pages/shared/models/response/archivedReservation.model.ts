import { IRoom } from "../Room"
import { ICurrency } from "../Currency"
export interface IArchivedReservation {
  from: Date;
  to: Date;
  reservationCommitteeFullName?: string;
  originalPrice: number;
  discount: number;
  totalPrice: number;
  currency: ICurrency;
  totalPriceInBAMAndEur: ITotalPriceInBAMAndEur;
  isCancelled: boolean;
  reservedRooms: IRoom[];
}
export interface ITotalPriceInBAMAndEur {
  totalPriceBam: number;
  totalPriceEur: number;
}
