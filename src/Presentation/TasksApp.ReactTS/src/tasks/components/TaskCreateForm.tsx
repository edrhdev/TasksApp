import { useState } from "react";
import { PlusCircle, Loader2 } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Card, CardContent } from "@/components/ui/card";

interface TaskCreateFormProps {
    onCreateTask: (title: string) => Promise<void>;
    disabled: boolean;
}

export function TaskCreateForm({ onCreateTask, disabled }: TaskCreateFormProps) {
    const [title, setTitle] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (e: React.SubmitEvent) => {
        e.preventDefault();

        const cleanTitle = title.trim();

        if (!cleanTitle || isSubmitting) return;

        try {
            setIsSubmitting(true);
            await onCreateTask(cleanTitle);
            setTitle("");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <Card className="p-0">
            <CardContent>
                <form onSubmit={handleSubmit} className="flex gap-2 py-4">
                    <Input
                        type="text"
                        placeholder="Write a new task..."
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        disabled={isSubmitting || disabled}
                        maxLength={100}
                        className="flex-1"
                    />
                    <Button type="submit" disabled={!title.trim() || isSubmitting || disabled}>
                        {(isSubmitting || disabled) ? (
                            <Loader2 className="w-4 h-4 animate-spin mr-2" />
                        ) : (
                            <PlusCircle className="w-4 h-4" />
                        )}
                        Add
                    </Button>
                </form>
            </CardContent>
        </Card>
    );
}