interface CardHomeProps {
  title: string;
  children: React.ReactNode;
}

export function CardHome({ title, children }: CardHomeProps) {
  return (
    <div className="rounded-lg border border-gray-200 bg-white p-6 shadow-md">
      <h2 className="mb-4 text-xl font-semibold text-gray-700">{title}</h2>
      <div>{children}</div>
    </div>
  );
}
