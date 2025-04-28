// Create a BaseLayout (no navigation) and MainLayout (with navigation)
import Navbar from './Navbar';
const BaseLayout = ({ children }) => (
  <div className="containter">
    {children}
  </div>
);

const MainLayout = ({ children }) => (
  <div className="containter py-4">
    <Navbar />
    {children}
  </div>
);

export { BaseLayout, MainLayout };