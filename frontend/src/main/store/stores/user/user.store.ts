import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const userStore = createSlice({
  name: "user",
  initialState: null as any,
  reducers: {
    setUser(_state, action: PayloadAction<any | null>) {
      return { ...action.payload, password: "" };
    },
  },
});

export default userStore;

export const { setUser } = userStore.actions;
