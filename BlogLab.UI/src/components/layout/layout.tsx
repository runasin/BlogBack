import React, { ReactNode } from 'react';
import Header from '../header/Header';

interface Props {
  children: ReactNode;
}

export default function Layout({ children }: Props): JSX.Element {
  return (
    <div>
      <Header />
      {children}
    </div>
  );
}