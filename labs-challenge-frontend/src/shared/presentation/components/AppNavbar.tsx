import React from "react";

interface AppNavbarProps {
  username: string;
}

export function AppNavbar({ username }: AppNavbarProps) {
  return (
    <nav className="flex items-center justify-between bg-gray-700 px-6 py-4 text-white shadow-md">
      <div className="text-lg font-semibold">Olá, {username}</div>
      <button
        className="rounded bg-blue-600 px-4 py-2 font-medium hover:bg-red-700 transition"
      >
        Sair
      </button>
    </nav>
  );
}
