import {
    Users,
    ClipboardList,
    CheckCircle2,
    AlertCircle,
    Package,
    LayoutDashboard,
    Truck,
    BarChart3,
    UserCircle2
} from 'lucide-react';
import { BarChart, Bar, XAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import './SystemAdmin_Dashboard.css';

const dailyDeliveries = [
    { day: 'Mon', weekday: 30, peak: 10 },
    { day: 'Tue', weekday: 25, peak: 15 },
    { day: 'Wed', weekday: 40, peak: 20 },
    { day: 'Thu', weekday: 35, peak: 12 },
    { day: 'Fri', weekday: 50, peak: 25 },
    { day: 'Sat', weekday: 20, peak: 8 },
    { day: 'Sun', weekday: 15, peak: 5 },
];

export default function Dashboard() {
    const employeeId = localStorage.getItem('employeeId') ?? '';

    return (
        <div className="dashboard-container">
            {/* Sidebar */}
            <aside className="sidebar">
                <div className="sidebar-logo">
                    <div className="logo-box"></div>
                </div>

                <nav className="sidebar-nav">
                    <div className="nav-item">
                        <LayoutDashboard size={22} />
                        <span>Dashboard</span>
                    </div>
                    <div className="nav-item active">
                        <Package size={22} />
                        <span>Manage</span>
                    </div>
                    <div className="nav-item">
                        <Truck size={22} />
                        <span>Delivery</span>
                    </div>
                    <div className="nav-item">
                        <BarChart3 size={22} />
                        <span>Analytics</span>
                    </div>
                    <div className="nav-item">
                        <UserCircle2 size={22} />
                        <span>Profile</span>
                    </div>
                </nav>
            </aside>

            {/* Main */}
            <main className="main-viewport">
                {/* Header */}
                <div className="dashboard-header">
                    <div>
                        <h2>Board Overview</h2>
                        <p>
                            Dashboard —{" "}
                            {new Date().toLocaleDateString('en-US', {
                                weekday: 'long',
                                year: 'numeric',
                                month: 'long',
                                day: 'numeric'
                            })}
                        </p>
                    </div>

                    {/* ✅ Improved User UI */}
                    <div className="header-user">
                        <div className="user-block">
                            <div className="avatar-circle">
                                {employeeId ? employeeId.charAt(0).toUpperCase() : 'E'}
                            </div>

                            <div className="user-text">
                                <span className="welcome-text">Welcome back</span>
                                <strong>{employeeId || 'Employee'}</strong>
                            </div>
                        </div>

                        <button
                            className="logout-btn"
                            onClick={() => {
                                localStorage.removeItem('employeeId');
                                window.location.href = '/login';
                            }}
                        >
                            Logout
                        </button>
                    </div>
                </div>

                <div className="dashboard-content">

                    {/* Stats */}
                    <div className="stats-row">

                        <div className="stat-card">
                            <div className="stat-icon bg-primary">
                                <Users size={18} />
                            </div>
                            <div>
                                <p>TOTAL EMPLOYEES</p>
                                <h3 className="skeleton"></h3>
                                <small>Current active staff</small>
                            </div>
                        </div>

                        <div className="stat-card">
                            <div className="stat-icon bg-warning">
                                <ClipboardList size={18} />
                            </div>
                            <div>
                                <p>ACTIVE TASKS</p>
                                <h3 className="skeleton"></h3>
                                <small>Pending & In Transit</small>
                            </div>
                        </div>

                        <div className="stat-card">
                            <div className="stat-icon bg-success">
                                <CheckCircle2 size={18} />
                            </div>
                            <div>
                                <p>TASKS COMPLETED</p>
                                <h3 className="skeleton"></h3>
                                <small>Total successful deliveries</small>
                            </div>
                        </div>

                        <div className="stat-card">
                            <div className="stat-icon bg-danger">
                                <AlertCircle size={18} />
                            </div>
                            <div>
                                <p>LOCKED ACCOUNTS</p>
                                <h3 className="skeleton"></h3>
                                <small>Needs admin action</small>
                            </div>
                        </div>

                    </div>

                    {/* Main Grid */}
                    <div className="dashboard-grid">

                        {/* Employees */}
                        <div className="card">
                            <div className="card-header">
                                <h3>Recent Employees</h3>
                                <a href="/employees" className="view-all-link">View all →</a>
                            </div>

                            <table className="data-table">
                                <thead>
                                    <tr>
                                        <th>NAME</th>
                                        <th>ID</th>
                                        <th>ROLE</th>
                                        <th>STATUS</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td colSpan={4}>
                                            <div className="empty-state">
                                                <Package size={20} />
                                                <p>No data available</p>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        {/* Activity */}
                        <div className="card">
                            <div className="card-header">
                                <h3>Recent Activity</h3>
                                <a href="/activity-logs" className="view-all-link">View All</a>
                            </div>

                            <div className="activity-feed-list">
                                <div className="empty-state">
                                    <ClipboardList size={20} />
                                    <p>No recent activity</p>
                                </div>
                            </div>
                        </div>

                    </div>

                    {/* Bottom */}
                    <div className="dashboard-bottom-row">

                        {/* Quick Actions */}
                        <div className="card">
                            <h3>Quick Actions</h3>

                            <div className="quick-actions-grid">

                                <button className="quick-action-btn primary">
                                    <div className="quick-action-icon">
                                        <Users size={20} />
                                    </div>
                                    <span>Add Employee</span>
                                </button>

                                <button className="quick-action-btn">
                                    <div className="quick-action-icon warning">
                                        <ClipboardList size={20} />
                                    </div>
                                    <span>Create Task</span>
                                </button>

                            </div>
                        </div>

                        {/* System Status */}
                        <div className="card">
                            <div className="card-header">
                                <h3>System Status</h3>
                                <span className="system-all-operational">All Operational</span>
                            </div>

                            <div className="system-status-list">

                                <div className="system-status-item">
                                    <div className="system-icon bg-primary">
                                        <Users size={16} />
                                    </div>
                                    <div className="system-info">
                                        <span className="system-name">Operation System</span>
                                        <span className="system-detail">— employees active</span>
                                    </div>
                                    <span className="system-uptime">99.9%</span>
                                </div>

                                <div className="system-status-item">
                                    <div className="system-icon bg-danger">
                                        <ClipboardList size={16} />
                                    </div>
                                    <div className="system-info">
                                        <span className="system-name">Delivery Management</span>
                                        <span className="system-detail">— total orders</span>
                                    </div>
                                    <span className="system-uptime">99.7%</span>
                                </div>

                                <div className="system-status-item">
                                    <div className="system-icon bg-success">
                                        <Package size={16} />
                                    </div>
                                    <div className="system-info">
                                        <span className="system-name">Delivery Tracker</span>
                                        <span className="system-detail">— active shipments</span>
                                    </div>
                                    <span className="system-uptime">98.2%</span>
                                </div>

                            </div>
                        </div>

                        {/* Chart */}
                        <div className="card">
                            <div className="card-header">
                                <h3>Delivery Performance</h3>
                                <span className="system-all-operational alt">This Week</span>
                            </div>

                            <div style={{ width: '100%', height: '220px', marginTop: '16px' }}>
                                <ResponsiveContainer>
                                    <BarChart data={dailyDeliveries}>
                                        <CartesianGrid strokeDasharray="3 3" vertical={false} />
                                        <XAxis dataKey="day" axisLine={false} tickLine={false} />
                                        <Tooltip />
                                        <Bar dataKey="weekday" radius={[4, 4, 0, 0]} />
                                        <Bar dataKey="peak" radius={[4, 4, 0, 0]} />
                                    </BarChart>
                                </ResponsiveContainer>
                            </div>
                        </div>

                    </div>
                </div>
            </main>
        </div>
    );
}