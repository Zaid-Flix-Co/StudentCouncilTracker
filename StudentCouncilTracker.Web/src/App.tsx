import { Link, Route, BrowserRouter as Router, Routes } from "react-router-dom";
import UsersPage from "./pages/UsersPage";
import EventsPage from "./pages/EventsPage";

function App() {
  return (
    <Router>
      <div className="app-container">
        <header className="app-header">
          <nav>
            <ul className="nav-list">
              <li>
                <Link to="/users">Справочник пользователей</Link>
              </li>
              <li>
                <Link to="/events">Справочник мероприятий</Link>
              </li>
            </ul>
          </nav>
        </header>

        <main>
          <Routes>
            <Route path="/users" element={<UsersPage />} />
            <Route path="/events" element={<EventsPage />} />
          </Routes>
        </main>

        <footer>
          {/* Add your footer content here */}
        </footer>
      </div>
    </Router>
  );
}

export default App;