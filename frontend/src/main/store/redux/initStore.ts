import { configureStore, current } from "@reduxjs/toolkit";
import { setUser } from "../stores/user/user.store";
import rootReducer from "./rootReducer";

const initStore = (currentUser: any) => {
  const appStore = configureStore({
    reducer: rootReducer,
  });
  if (currentUser) appStore.dispatch(setUser(currentUser));
  return appStore;
};

export default initStore;
