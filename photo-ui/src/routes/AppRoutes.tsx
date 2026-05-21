import {
  BrowserRouter,
  Routes,
  Route,
} from 'react-router-dom';

import HomePage from '../pages/HomePage';
import CreatePhotoPage from '../pages/CreatePhotoPage';

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={<HomePage />}
        />

        <Route
          path="/create"
          element={<CreatePhotoPage />}
        />
      </Routes>
    </BrowserRouter>
  );
}