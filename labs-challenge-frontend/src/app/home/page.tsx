import { AppFooter } from "@/shared/presentation/components/AppFooter";
import { AppNavbar } from "@/shared/presentation/components/AppNavbar";
import { CardHome } from "./components/CardHome";

export default function HomePage() {
  const user = "Gian";
  if (!user) {
    return (
      <main className="flex min-h-screen flex-col items-center justify-center">
        <p>Usuário não autenticado.</p>
      </main>
    );
  }
  return (
    <div className="flex min-h-screen flex-col">
      <AppNavbar username={user} />
      <main className="flex-grow bg-gradient-to-tr from-blue-100 via-white to-green-100 p-8">
        <h1 className="mb-8 text-center text-4xl font-extrabold text-gray-800">
          Dashboard Logístico
        </h1>
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          <CardHome title="Entregas Pendentes">
            <p className="text-3xl font-bold text-green-600">24</p>
            <p className="mt-2 text-gray-600">
              Entregas aguardando despacho para clientes.
            </p>
          </CardHome>
          <CardHome title="Pedidos em Trânsito">
            <p className="text-3xl font-bold text-yellow-600">13</p>
            <p className="mt-2 text-gray-600">
              Pedidos atualmente em rota de entrega.
            </p>
          </CardHome>
          <CardHome title="Estoque Disponível">
            <p className="text-3xl font-bold text-blue-600">1.502</p>
            <p className="mt-2 text-gray-600">Itens prontos para envio.</p>
          </CardHome>
          <CardHome title="Notificações Recentes">
            <ul className="list-disc space-y-1 pl-5 text-gray-700">
              <li>Entrega #423 atrasada 1 dia.</li>
              <li>Pedido #982 confirmado para expedição.</li>
              <li>Estoque do item X abaixo do mínimo.</li>
            </ul>
          </CardHome>
        </div>
      </main>
      <AppFooter />
    </div>
  );
}
