import React from 'react'

type Props = {}

export default function Navbar({}: Props) {
  return (
   <div className="navbar bg-base-100 shadow-sm">
        <a className="btn btn-ghost text-xl" href="/">
          Shorten
        </a>
      </div>
  )
}