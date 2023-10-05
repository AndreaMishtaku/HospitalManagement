import React, { Suspense } from "react";
import ReactDOM from "react-dom/client";
import { Provider } from "react-redux";
import App from "./app/App";
import initApp from "./main/initializers/app";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "./scss/volt.scss";
import "react-datetime/css/react-datetime.css";

initApp().then((appStore: any) => {
  const root = ReactDOM.createRoot(document.getElementById("root"));

  root.render(
    <Provider store={appStore}>
      <Suspense fallback="loading">
        <ToastContainer
          autoClose={5000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
        <App />
      </Suspense>
    </Provider>
  );
});
