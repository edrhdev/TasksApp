import { CheckCircle2 } from "lucide-react";

function TasksHeader() {
    return (
        <header className="text-center space-y-3">
            <div className="inline-flex items-center justify-center p-3 bg-primary/10 rounded-full text-primary">
                <CheckCircle2 className="w-8 h-8" />
            </div>
            <h1 className="text-3xl font-bold tracking-tight">Tasks Dashboard</h1>
            <p className="text-sm text-muted-foreground">
                Enterprise Task Management built with React TS & .NET
            </p>
        </header>
    );
}

export default TasksHeader;