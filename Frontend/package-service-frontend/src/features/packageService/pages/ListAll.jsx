import { ListOrder } from "../components";
import { IoArrowBackCircle } from "react-icons/io5";

export const ListAll = () => {
  return (
    <div>
      <IoArrowBackCircle className="w-10 h-16" />
      <div className="max-w-none w-full h-full justify-center">
        <ListOrder/>
      </div>
    </div>
  );
};