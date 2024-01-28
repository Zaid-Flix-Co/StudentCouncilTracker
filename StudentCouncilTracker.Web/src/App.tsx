import {
  BrowserRouter as Router,
  Route,
  Link,
  Routes,
} from "react-router-dom";
import UsersPage from "./pages/UsersPage";
import EventsPage from "./pages/EventsPage";

function App() {
  return (
    <Router>
      <div>
        <header>
          <nav>
            <ul>
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
      </div>
    </Router>
  );
}

export default App;
