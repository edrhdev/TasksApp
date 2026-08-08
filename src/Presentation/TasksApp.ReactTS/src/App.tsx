import { useTasks } from "./tasks/hooks/useTasks";
import { TaskCreateForm } from "./tasks/components/TaskCreateForm";
import { TaskList } from "./tasks/components/TaskList";
import { Card } from "./components/ui/card";
import TasksCounterTab from "./tasks/components/TasksCounterTab";
import TasksHeader from "./tasks/components/TasksHeader";

export function App() {
    const { tasks, isLoading, refreshTasks, createTask, toggleTaskStatus, isToggling, deleteTask, isDeleting } = useTasks();

    return (
        <div className="min-h-screen bg-background text-foreground py-12 px-4">
            <main className="max-w-2xl mx-auto space-y-8">
                <Card className="p-6">
                    {/* Tasks App Header */}
                    <TasksHeader />

                    {/* Task Creation Form */}
                    <TaskCreateForm onCreateTask={createTask} disabled={isLoading || isToggling || isDeleting} />

                    {/* Tasks Counter Component */}
                    <TasksCounterTab tasks={tasks} isBusy={isLoading || isToggling || isDeleting} refreshTasks={refreshTasks} />

                    {/* Tasks List Component */}
                    <TaskList
                        tasks={tasks}
                        onToggle={toggleTaskStatus}
                        onDelete={deleteTask}
                        isLoading={isLoading}
                        isToggling={isToggling}
                        isDeleting={isDeleting}
                    />
                </Card>
            </main>
        </div >
    );
}

export default App
