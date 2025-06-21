import React from "react";
import { AppButton } from "@/shared/presentation/components/AppButton";
import { InputField } from "@/shared/presentation/components/InputField";

export default function LoginPage() {
  return (
    <div className="w-full max-w-md rounded-lg bg-white p-8 shadow-md">
      <h1 className="mb-6 text-center text-3xl font-bold text-gray-800">
        Logistica Admin
      </h1>
      <form className="space-y-4">
        <InputField
          label="E-mail"
          type="email"
          id="email"
          placeholder="seuemail@email.com"
        />
        <InputField
          label="Senha"
          type="password"
          id="password"
          placeholder="********"
        />
        <AppButton type="submit" className="w-full">
          Entrar
        </AppButton>
      </form>
      <p className="mt-4 text-center text-sm text-gray-600">
        Esqueceu sua senha?{" "}
        <a href="#" className="text-blue-600 hover:underline">
          Recuperar
        </a>
      </p>
    </div>
  );
}
