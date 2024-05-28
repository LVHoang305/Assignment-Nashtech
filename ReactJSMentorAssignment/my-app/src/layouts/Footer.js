import React from "react";

const Footer = () => {
  const socials = [
    {
      name: "twitter",
      path: "https://www.twitter.com/",
    },
    {
      name: "youtube",
      path: "https://www.youtube.com/",
    },
    {
      name: "instagram",
      path: "https://www.instagram.com/",
    },
    {
      name: "github",
      path: "https://github.com/",
    },
  ];
  const menus = [
    {
      name: "useful links",
      items: [
        { name: "About Us", path: "/home" },
        { name: "Blog", path: "/home" },
        {
          name: "Github",
          path: "/home",
        },
        {
          name: "Link",
          path: "/home",
        },
      ],
    },
    {
      name: "other resources",
      items: [
        {
          name: "Something1",
          path: "/home",
        },
        {
          name: "Something2",
          path: "/home",
        },
        {
          name: "Something3",
          path: "/home",
        },
        {
          name: "Something4",
          path: "/home",
        },
      ],
    },
  ];

  return (
    <footer className="relative px-4 pt-8 pb-6 bg-gray-800 text-white w-full">
      <div className="container mx-auto flex flex-col justify-between h-full">
        <div className="flex flex-wrap pt-6 text-center lg:text-left">
          <div className="w-full px-4 lg:w-6/12">
            <h2 className="text-lg font-bold mb-4">NashTech Post</h2>
            <p className="text-sm text-blue-gray-500 mb-6">
              WebApp to display NashTech's Post
            </p>
            <div className="flex justify-center gap-2 md:justify-start">
              {socials.map(({ name, path }) => (
                <a
                  key={name}
                  href={path}
                  target="_blank"
                  rel="noopener noreferrer"
                  className="text-white hover:text-gray-400"
                >
                  <i className={`fab fa-${name}`} />
                </a>
              ))}
            </div>
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
