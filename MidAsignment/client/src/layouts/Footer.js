import React from "react";

const Footer = () => {
  const menus = [
    {
      name: "useful links",
      items: [
        { name: "About Us", path: "/home" },
        { name: "Terms & Conditions", path: "/home" },
        {
          name: "Privacy Policy",
          path: "/home",
        },
        {
          name: "Rules & Regulations",
          path: "/home",
        },
      ],
    },
    {
      name: "Our links",
      items: [
        {
          name: "Twitter",
          path: "https://www.twitter.com/",
        },
        {
          name: "Youtube",
          path: "https://www.youtube.com/",
        },
        {
          name: "Instagram",
          path: "https://www.instagram.com/",
        },
        {
          name: "Github",
          path: "https://github.com/",
        },
      ],
    },
  ];

  return (
    <footer className="relative px-4 pt-8 pb-6 bg-gray-800 text-white w-full">
      <div className="container mx-auto flex flex-col justify-between h-full">
        <div className="flex flex-wrap pt-6 text-center lg:text-left">
          <div className="w-full px-4 lg:w-6/12">
            <h2 className="text-lg font-bold mb-4">NashTech Library</h2>
            <p className="text-sm text-blue-gray-500 mb-6">
              WebApp to manage NashTech Library
            </p>
          </div>
          <div className="mx-auto mt-12 grid grid-cols-2 gap-24 lg:mt-0">
            {menus.map(({ name, items }) => (
              <div key={name}>
                <h3 className="text-sm font-medium mb-2 uppercase text-blue-gray-500">
                  {name}
                </h3>
                <ul>
                  {items.map((item) => (
                    <li key={item.name}>
                      <a
                        href={item.path}
                        target="_blank"
                        rel="noreferrer"
                        className="text-sm text-blue-gray-500 hover:text-blue-gray-700"
                      >
                        {item.name}
                      </a>
                    </li>
                  ))}
                </ul>
              </div>
            ))}
          </div>
        </div>
        <hr className="my-6 border-gray-300" />
        <div className="flex justify-center items-center">
          <p className="text-sm text-blue-gray-500">
            <>
              Copyright © 2024 React WebApp by{" "}
              <a
                href="https://www.nashtechglobal.com/"
                target="_blank"
                rel="noopener noreferrer"
                className="text-blue-gray-500 hover:text-red-500 transition-colors"
              >
                NashTech
              </a>
              .
            </>
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
