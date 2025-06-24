import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/home/interactor/actions/home_actions.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final _actions = Modular.get<HomeActions>();

  @override
  void initState() {
    _actions.getInfoUser();
    if (_actions.currentState.user != null && !_actions.currentState.user!.isEmailConfirmed) {
      _actions.logout("Cadastro pendente de confirmação, por favor verifique o token enviado ao email de cadastro", false);
    }
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    final state = _actions.currentState;
    return Scaffold(
      backgroundColor: Colors.blue.shade50,
      appBar: AppBar(
        title: const Text('Logistica App'),
        backgroundColor: Colors.blue,
        actions: [
          PopupMenuButton<int>(
            icon: const Icon(Icons.account_circle, size: 30),
            onSelected: (value) {
              if (value == 1) {
                _actions.logout("Logout efetuado com sucesso", true);
              }
            },
            itemBuilder:
                (context) => [
                  PopupMenuItem(
                    value: 0,
                    enabled: false,
                    child: Row(
                      children: [
                        const Icon(Icons.person, color: Colors.grey),
                        const SizedBox(width: 8),
                        Text(state.user!.name),
                      ],
                    ),
                  ),
                  const PopupMenuDivider(),
                  const PopupMenuItem(
                    value: 1,
                    child: Row(
                      children: [
                        Icon(Icons.logout, color: Colors.red),
                        SizedBox(width: 8),
                        Text("Sair"),
                      ],
                    ),
                  ),
                ],
          ),
        ],
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                _buildSummaryCard(
                  icon: Icons.local_shipping_outlined,
                  title: 'Entregas',
                  value: '120',
                  color: Colors.blue,
                ),
                const SizedBox(width: 16),
                _buildSummaryCard(
                  icon: Icons.pending_actions_outlined,
                  title: 'Pendentes',
                  value: '25',
                  color: Colors.orange,
                ),
              ],
            ),
            const SizedBox(height: 16),
            Row(
              children: [
                _buildSummaryCard(
                  icon: Icons.check_circle_outline,
                  title: 'Concluídas',
                  value: '95',
                  color: Colors.green,
                ),
                const SizedBox(width: 16),
                _buildSummaryCard(
                  icon: Icons.error_outline,
                  title: 'Atrasadas',
                  value: '5',
                  color: Colors.red,
                ),
              ],
            ),
            const SizedBox(height: 32),
            const Text(
              'Entregas Recentes',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.w600),
            ),
            const SizedBox(height: 16),
            _buildDeliveryItem(
              title: 'Pedido #12345',
              subtitle: 'Em trânsito - São Paulo',
              status: 'Em trânsito',
              statusColor: Colors.blue,
            ),
            _buildDeliveryItem(
              title: 'Pedido #12346',
              subtitle: 'Entregue - Rio de Janeiro',
              status: 'Entregue',
              statusColor: Colors.green,
            ),
            _buildDeliveryItem(
              title: 'Pedido #12347',
              subtitle: 'Atrasado - Belo Horizonte',
              status: 'Atrasado',
              statusColor: Colors.red,
            ),
            _buildDeliveryItem(
              title: 'Pedido #12348',
              subtitle: 'Pendente - Curitiba',
              status: 'Pendente',
              statusColor: Colors.orange,
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildSummaryCard({
    required IconData icon,
    required String title,
    required String value,
    required Color color,
  }) {
    return Expanded(
      child: Container(
        padding: const EdgeInsets.all(16),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(12),
          boxShadow: const [
            BoxShadow(
              color: Colors.black12,
              blurRadius: 8,
              offset: Offset(0, 4),
            ),
          ],
        ),
        child: Column(
          children: [
            Icon(icon, size: 36, color: color),
            const SizedBox(height: 8),
            Text(
              value,
              style: TextStyle(
                fontSize: 24,
                fontWeight: FontWeight.bold,
                color: color,
              ),
            ),
            const SizedBox(height: 4),
            Text(
              title,
              style: const TextStyle(fontSize: 14, color: Colors.black54),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildDeliveryItem({
    required String title,
    required String subtitle,
    required String status,
    required Color statusColor,
  }) {
    return Container(
      margin: const EdgeInsets.only(bottom: 12),
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(12),
        boxShadow: const [
          BoxShadow(color: Colors.black12, blurRadius: 6, offset: Offset(0, 3)),
        ],
      ),
      child: ListTile(
        contentPadding: EdgeInsets.zero,
        title: Text(title, style: const TextStyle(fontWeight: FontWeight.w600)),
        subtitle: Text(subtitle),
        trailing: Container(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(20),
            border: Border.all(color: statusColor),
          ),
          child: Text(
            status,
            style: TextStyle(
              color: statusColor,
              fontWeight: FontWeight.w600,
              fontSize: 12,
            ),
          ),
        ),
      ),
    );
  }
}
