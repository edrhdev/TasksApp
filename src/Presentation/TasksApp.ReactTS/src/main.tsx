import { StrictMode } from "react"
import { createRoot } from "react-dom/client"
import App from "./App.tsx"
import "./index.css"
import { Toaster } from '@/components/ui/sonner'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            retry: 0,
            refetchOnWindowFocus: false,
        },
    },
})

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <QueryClientProvider client={queryClient}>
            <div className="min-h-screen flex items-center justify-center">
                <App />
                <Toaster position="bottom-right" richColors />
            </div>
        </QueryClientProvider>
    </StrictMode>
)
