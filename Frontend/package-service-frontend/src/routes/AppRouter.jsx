import { Routes, Route } from 'react-router-dom';
import { PackageServiceRouter } from '../features/packageService/routes/PackageServiceRouter';

export const AppRouter = () => {
  return (
    <Routes>
      <Route path="*" element={<PackageServiceRouter />} />
    </Routes>
  );
};
