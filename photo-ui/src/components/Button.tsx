import type { ButtonHTMLAttributes } from 'react';

interface Props
  extends ButtonHTMLAttributes<HTMLButtonElement> {
  isLoading?: boolean;
}

export default function Button({
  children,
  isLoading,
  ...props
}: Props) {
  return (
    <button
      {...props}
      disabled={isLoading || props.disabled}
      className="
        bg-blue-600
        text-white
        px-4
        py-2
        rounded-lg
        hover:bg-blue-700
        disabled:opacity-50
      "
    >
      {isLoading ? 'Loading...' : children}
    </button>
  );
}