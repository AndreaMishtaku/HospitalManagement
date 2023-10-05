import axiosInit from "./axios";
import initStore from "../store/redux/initStore";
import AuthManager from "../utils/authManager";
import JwtManager from "../utils/jwtManager";
import initIcons from "./icons";

const initApp = async () => {
  await axiosInit();
  let currentUser = null;
  try {
    if (JwtManager.accessToken) {
      currentUser = await AuthManager.parseJwt(JwtManager.accessToken);
    }
  } catch (e) {
    console.log("JwtManager.accessToken");
  }

  const appStore = initStore(currentUser);
  initIcons();
  return appStore;
};
export default initApp;
